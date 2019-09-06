<template>
  <div class="home">
    <!-- <img alt="Vue logo" src="../assets/logo.png"> -->
    <p style="font-size:20px;">Get token from JAONP</p>
    <p style="word-wrap: break-word;">{{token}}</p>
    <!-- <HelloWorld msg="Welcome to Your Vue.js App"/> -->
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import { debuglog } from 'util';

export default {
  name: 'home',
  data(){
    return {
      token:""
    }
  },
  components: {
    HelloWorld
  },
  methods:{
    vueJsonpShopList(params) {

      this.$jsonp('http://localhost:5000/api/Login/GetjsonpForVue',params)
          .then(res => {
              // var jdata = eval("("+res+")");
              console.log(res)
              this.token=res;
      })
    },
    callBackFunc(data){
      console.log(data)
    }
  },
  mounted(){
    this.vueJsonpShopList();
  }
  
}
</script>
