<template>
  <div>
    <v-overlay v-if="$store.getters.isLoading">
      <lottie-animation
        path="loading.json"
        :loop="true"
        :autoPlay="true"
        :speed="1"
        :width="256"
        :height="256"
      />
    </v-overlay>
    <router-view></router-view>
  </div>
</template>

<script>
import LottieAnimation from "lottie-vuejs/src/LottieAnimation.vue"; // import lottie-vuejs
export default {
  name: "App",
  components: {
    "lottie-animation": LottieAnimation,
  },
  data: () => ({
    //
  }),
};
</script>

<style>
.row {
  margin: 0px;
}
.col {
  padding: 0px;
}
.gradient {
  background: rgb(52, 52, 76);
  background: linear-gradient(
    90deg,
    rgba(52, 52, 76, 1) 0%,
    rgba(19, 156, 148, 1) 35%,
    rgba(32, 76, 116, 1) 100%
  );
}
</style>