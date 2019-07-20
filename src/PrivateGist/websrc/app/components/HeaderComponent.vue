<template>
  <nav id="mainNavBar" class="navbar navbar-dark bg-dark">
    <button class="navbar-toggler navbar-dark" type="button" v-on:click="$emit('toggle-sidebar')">
      <span class="navbar-toggler-icon"></span>
    </button>
    <a class="navbar-brand" href="#">{{title}}</a>
    <div style="flex-grow: 1"></div>
  </nav>
</template>
<script>
export default {
  props: ['title'],
  data() {
    return {
    };
  }
};
</script>
<style lang="scss">
#mainNavBar > a {
  margin-left: 10px;
}
</style>


