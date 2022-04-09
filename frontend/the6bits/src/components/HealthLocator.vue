<template>
  <div class="form">
    <div>
    
      <H1>View Health Location</H1>
      {{ message }}
    </div>

    <button @click="view">View</button>

    <button @click="locations">Search</button>
  </div>
</template>

<script>
export default {
  name: "viewHL",
  data() {
    return {
      message: "",
    };
  },
  methods: {
    locations() {
      const requestOptions = {
        method: "GET",
        credentials: "include",
      };
      fetch("https://localhost:7011/HealthLocator/viewHL", requestOptions)
        .then((response) => response.json())
        // .then((data) => data.value[index].url);
        .then((body) => (this.message = body));
    },
  },
};
</script>