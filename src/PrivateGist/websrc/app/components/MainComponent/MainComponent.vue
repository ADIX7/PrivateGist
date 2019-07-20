<script>
export default {
  inject: ["signalRApi", "signalRStateChanged"],
  data() {
    return {
      gists: ["asd1", "asd2"]
    };
  },
  methods: {
    processGistsByUser(data) {
      console.log(data);
    },
    getUpdate() {
      console.log("update");
      this.signalRApi.methods.getGistsByUser("DummyUser");
    }
  },
  created: function() {
    this.signalRApi.observables.gistsByUser.subscribe({
      next: data => this.processGistsByUser(JSON.parse(data)),
      error: err => console.error("something wrong occurred: " + err),
      complete: () => console.log("done")
    });
    this.signalRStateChanged.subscribe({
      next: data => this.getUpdate(),
      error: err => console.error("SignalR error: " + err),
      complete: () => console.log("SignalR connection closed.")
    });
  }
};
</script>

<template src="./MainComponent.html"></template>
<style lang="scss" src="./MainComponent.scss"></style>
