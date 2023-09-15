<script setup>
import MoviesTable from './components/MoviesTable.vue';
</script>

<template>
	<div class="container">
		<h1>My Movies Collection</h1>
		<br />
		<MoviesTable />
	</div>
</template>

<style scoped>
.hidden {
	display: none;
}
</style>
