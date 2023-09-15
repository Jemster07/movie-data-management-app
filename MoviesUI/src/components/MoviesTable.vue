<template>
	<div id="moviesTable">

		<!-- Button to add new movie -->
		<button data-bs-toggle="modal" data-bs-target="#staticBackdrop" v-on:click="addMovieClicked">Add New Movie</button>

		<table class="table table-dark table-striped table-hover">
			<thead class="thead-light">
				<tr>
					<th>Name</th>
					<th>Description</th>
					<th>Release Year</th>
					<th>Academy Award</th>
					<th>Director ID</th>
					<th style="min-width: 120px" scope="col"></th>
				</tr>
			</thead>
			<tbody>
				<tr v-for="movie in movies" :key="movie.id">
					<td>{{ movie.name }}</td>
					<td>{{ movie.description }}</td>
					<td>{{ movie.releaseYear }}</td>
					<td>{{ movie.academyAward }}</td>
					<td>{{ movie.directorId }}</td>
					<td>
						<span data-bs-toggle="modal" data-bs-target="#staticBackdrop" v-on:click="editMovieClicked(movie)"
							class="clickable m-2">
							<font-awesome-icon icon="fa-solid fa-pen-to-square" />
						</span>
						<span class="clickable m-2" v-on:click="deleteMovie(movie)">
							<font-awesome-icon icon="fa-solid fa-trash" />
						</span>
					</td>
				</tr>
			</tbody>
		</table>

		<!-- Modal -->
		<div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
			aria-labelledby="staticBackdropLabel" aria-hidden="true">
			<div class="modal-dialog">
				<div class="modal-content">
					<div class="modal-header">
						<h1 class="modal-title fs-5" id="staticBackdropLabel">{{ formTitle }}</h1>
					</div>
					<div class="modal-body">

						<form v-on:submit.prevent="toggleSaveSubmit">
							<div class="formRows">
								<label for="formName">Name:</label>
								<input id="formName" name="newName" type="text" required min="1" max="50" class="formFields"
									v-model="formMovie.name" />
							</div>
							<div class="formRows">
								<label for="formDescription">Description:</label>
								<textarea id="formDescription" name="newDescription" min="0" max="500" class="formFields"
									v-model="formMovie.description"></textarea>
							</div>
							<div class="formRows">
								<label for="formReleaseYear">Release Year:</label>
								<input id="formReleaseYear" name="newReleaseYear" type="text" pattern="[0-9]{4}" required
									class="formFields" v-model="formMovie.releaseYear" />
							</div>
							<div class="formRows">
								<label for="formAcademyAward">Academy Award Winner?</label>
								<input id="formAcademyAward" name="academyAwardWinner" type="checkbox"
									:value="checkboxValue" v-on:change="toggleCheckboxValue" class="formFields"
									v-model="formMovie.academyAward" />
							</div>
							<div class="formRows">
								<label for="formDirectorId">Director: </label>
								<input id="formDirectorId" name="newDirectorId" type="number" class="formFields"
									v-model="formMovie.directorId" />
							</div>
							<button v-if="!showButton" type="submit" class="formFields">Submit</button>
							<button v-if="showButton" v-on:click="toggleFormMethod" data-bs-dismiss="modal" class="formFields">Are You Sure?</button>
							<button data-bs-dismiss="modal" class="formFields">Cancel</button>
						</form>

					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import { api_getAll, api_post, api_put, api_delete } from "../api.js";

export default {
	name: 'moviesTable',
	data() {
		return {
			movies: [],
			showButton: false,
			checkboxValue: "0",
			buttonTrigger: "",
			formTitle: "[ Form ]",
			formMovie: {
				id: 0,
				name: "",
				description: "",
				releaseYear: 0,
				academyAward: false,
				directorId: null,
			},
		}
	},
	async mounted() {
		this.getMovies();
	},
	methods: {
		async getMovies() {
			let response = await api_getAll();

			if (response == null) {
				console.log("There was an error loading the list of movies.");
			} else {
				this.movies = response;
			}
		},
		async deleteMovie(movie) {
			if (confirm(`Are you sure you want to delete ${movie.name}?`)) {
				let id = movie.id;
				api_delete(id).then(response => {
					if (response) {
						alert("Movie deleted!");
						this.resetForm();
					} else {
						alert(`An error occurred while deleting ${movie.name}.`);
					}
				})
					.catch(error => {
						console.log(error);
					})
			}
		},
		async insertMovie() {
			await api_post(this.formMovie).then(response => {
				if (response) {
					this.resetForm();
				} else {
					console.log(`There was an error saving ${this.formMovie.name}.`);
				}
			})
				.catch(error => {
					console.log(error);
				})
		},
		async updateMovie() {
			await api_put(this.formMovie).then(response => {
				if (response) {
					this.resetForm();
				} else {
					console.log(`There was an error updating ${this.formMovie.name}.`);
				}
			})
				.catch(error => {
					console.log(error);
				})
		},
		toggleCheckboxValue() {
			if (this.checkboxValue == "0") {
				this.checkboxValue = "1";
			} else {
				this.checkboxValue = "0";
			}
		},
		toggleSaveSubmit() {
			this.showButton = true;
		},
		resetForm() {
			this.formMovie = {};
			this.getMovies();
		},
		addMovieClicked() {
			this.formMovie = {};
			this.formTitle = "Add a movie";
			this.buttonTrigger = "addMovie";
		},
		editMovieClicked(movie) {
			this.formMovie = {};
			this.formMovie.id = movie.id;
			this.formTitle = `Edit ${movie.name}`;
			this.buttonTrigger = "editMovie";
		},
		toggleFormMethod() {
			this.showButton = false;

			if (this.buttonTrigger === "addMovie") {
				this.insertMovie();
			}
			else // this.buttonTrigger === "editMovie"
			{
				this.updateMovie();
			}
		},
	},
}
</script>

<style scoped>
.clickable {
	cursor: pointer;
	user-select: none;
}

.formRows {
	vertical-align: center;
	margin: 10px 0;
}

.formFields {
	margin: 0 5px 0 10px;
}
</style>
