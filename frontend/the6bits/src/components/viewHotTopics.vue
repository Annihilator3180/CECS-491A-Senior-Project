<template>
  <div class="viewHT">
    <div>
      <H1>View Hot Topics</H1>
      {{ message }}
    </div>
    <!-- <ul id="example-1">
      <li v-for="index in items" :key="index.message">
        {{ index.message }}
      </li>
    </ul> -->
    <button @click="news">Save</button>
  </div>
</template>

<script>
export default {
  name: "viewHT",
  data() {
    return {
      message: "",
    };
  },
  methods: {
    news() {
      const requestOptions = {
        method: "GET",
        credentials: "include",
      };
      fetch("https://localhost:7011/News/viewHT", requestOptions)
        .then((response) => response.json())
        // .then((data) => data.value[index].url);
        .then((body) => (this.message = body));
    },
  },
};
</script>
