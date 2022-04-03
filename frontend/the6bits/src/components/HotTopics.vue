<template>
  <br />
  <div class="form">
    <div class="ViewNews">
      <H1>View Hot Topics</H1>
      <button @click="ViewArticles">Get Hot Topics</button>
      {{ message }}
    </div>

    <div class="SearchNews">
      <h1>Search for a News Article</h1>
      <input type="text" placeholder="Search for an Article" />
      <button @click="SearchArticle">Search</button>
    </div>

    <div class="FavoriteNews">
      <br />
      <h1>Access Favorite Articles</h1>
      <button @click="FavoriteArticle">Get Favorite Articles</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "ViewHT",
  data() {
    return {
      message: "",
    };
  },
  methods: {
    ViewArticles() {
      const requestOptions = {
        method: "get",
        credentials: "include",
      };
      fetch("https://localhost:7011/News/viewHT", requestOptions)
        .then((response) => response.json())
        // .then((data) => data.value[index].url);
        .then((body) => (this.message = body));
    },

    FavoriteArticle() {
      const requestOptions = {
        method: "get",
        credentials: "include",
      };
      fetch("", requestOptions);
    },

    SearchArticle() {
      const requestOptions = {
        method: "get",
        credentials: "include",
      };
      fetch("https://www.cnn.com/");
    },
  },
};
</script>
