<template>
 <v-app>
    <v-main>
      <ciceksepeti-toolbar :show="sidebarShow" v-on:sidebarClosed="sidebarStatusChanged"></ciceksepeti-toolbar>
      <ciceksepeti-sidebar :show="sidebarShow" v-on:sidebarClosed="sidebarClosed"></ciceksepeti-sidebar>
      <v-main class="pa-0">
        <router-view></router-view>
      </v-main>
    </v-main>
  </v-app>
</template>

<script>
export default{
  data:()=> ({
    sidebarShow : true
  }),
  methods:{
    sidebarStatusChanged(){
      this.sidebarShow = !this.sidebarShow;
    },
    sidebarClosed(val){
      this.sidebarShow = val;
    }
  }
}
</script>