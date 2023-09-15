const API_URL = "http://localhost:42386/api/Movies";

async function api_getAll() {
  let response;
  try {
    response = await fetch(API_URL);
  } catch (error) {
    console.error(error);
  } finally {
    if (!response || !response.ok) {
      return null;
    } else return response.json();
  }
}

async function api_post(payload) {
  let response;
  try {
    response = await fetch(API_URL, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });
  } catch (error) {
    console.error(error);
  } finally {
    if (!response || !response.ok) {
      return false;
    } else return true;
  }
}

async function api_put(payload) {
  let response;
  try {
    response = await fetch(API_URL + `/${payload.id}`, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });
  } catch (error) {
    console.error(error);
  } finally {
    if (!response || !response.ok) {
      return false;
    } else return true;
  }
}

async function api_delete(id) {
  let response;
  try {
    response = await fetch(API_URL + `/${id}`, { method: "DELETE" });
  } catch (error) {
    console.error(error);
  } finally {
    if (!response || !response.ok) {
      return false;
    } else return true;
  }
}

export { api_getAll, api_post, api_put, api_delete };
