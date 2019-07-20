<script>
import HeaderComponent from "../HeaderComponent.vue";
import MainComponent from "../MainComponent/MainComponent.vue";

export default {
  components: {
    HeaderComponent,
    MainComponent,
  },
  data() {
    return {
      currentView: "main",
      currentStop: "",
      currentTrip: "",
      title: "Main page"
    };
  },
  methods: {
    navigateToStop(event) {
      this.currentStop = event;
      this.currentView = "stop";
    },
    navigateToTrip(event) {
      this.currentTrip = event;
      this.currentView = "trip";
    }
  }
};
</script>

<template src="./AppComponent.html"></template>

<style lang="scss">
</style>


