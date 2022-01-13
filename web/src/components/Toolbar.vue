<template>
  <div>
    <v-app-bar class="gradient" height="52">
      <v-app-bar-nav-icon @click="sidebarStatusChange">
        <v-icon color="white">fa fa-bars</v-icon>
      </v-app-bar-nav-icon>
      <v-toolbar-title class="pa-0" v-if="!show">
        <span class="white--text">CicekSepeti Entegrasyon</span>
      </v-toolbar-title>
      <v-row class="ml-2">
        <v-spacer></v-spacer>
        <!-- Settings Menu!-->
        <v-menu offset-y>
          <template v-slot:activator="{ on, attrs }">
            <v-avatar class="avatar mr-1" v-on="on" v-bind="attrs">
              <v-img
                :src="require('../assets/avatar.png')"
                max-width="62"
                max-height="62"
              ></v-img>
            </v-avatar>
          </template>

          <v-list>
            <v-list-item @click="logout">
              <v-row class="ma-0"> Çıkış Yap </v-row>
            </v-list-item>
          </v-list>
        </v-menu>
        <!-- Settings Menu!-->
      </v-row>
    </v-app-bar>
  </div>
</template>

<script>
import { DESTROY_USER } from "../store/modules/auth/actions.type";
import { ShowErrorMessage } from "../common/Alerts";
export default {
  props: {
    show: {
      required: true,
      type: Boolean,
    },
  },
  methods: {
    sidebarStatusChange() {
      this.$emit("sidebarClosed");
    },
    logout() {
      this.$store
        .dispatch(DESTROY_USER)
        .then(() => {
          window.location.reload();
        })
        .catch((err) => {
          ShowErrorMessage(err.message);
        });
    },
  },
};
</script>

<style scoped>
.v-toolbar__content {
  margin: 0px !important;
  padding: 0px !important;
}
.avatar {
  cursor: pointer;
}
.v-list-item {
  cursor: pointer;
}
</style>