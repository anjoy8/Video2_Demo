<template>
  <div class="home">
    <p class="title">Get Data from JAONP</p>
    <p class="content">{{userJSONP}}</p>
    <hr>
    <p class="title">Get Data from Proxy</p>
    <p class="content">{{userProxy}}</p>
    <hr>
    <p class="title">Get Data from CORS</p>
    <p class="content">{{userCORS}}</p>
    <hr>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import axios from 'axios'
import { debuglog } from 'util';

export default {
  name: 'home',
  data(){
    return {
      userJSONP:"",
      userProxy:"",
      userCORS:"",
    }
  },
  methods:{
    //1、安装 vue-jsonp，并在main.js引用
    //2、直接发送请求，
    //3、注意只能是get，且接口输出方式是 response
    getDataByJSONP(params) {

      this.$jsonp('http://localhost:5000/api/Login/TestjsonpForVue',params)
          .then(res => {
              this.userJSONP=res;
      })
    },
    //1、配置vue.config.js 跨域代理，注意规则
    //2、发送任意请求
    //3、注意是相对路径，或者指向前端项目的域名5401
    getDataByProxy(params){
      //baseurl
      axios.post("/api/Login/TestProxyForVue").then((res)=>{
              this.userProxy=res;
      });
    },
    //1、后端配置cors代理
    //2、发送任意请求，
    //3、注意不能是相对路径
    getDataByCORS(params){
      axios.put("http://localhost:5000/apicors/Login/TestCORSForVue").then((res)=>{
              this.userCORS=res;
      });
    }
  },
  mounted(){
    this.getDataByJSONP();
    this.getDataByProxy();
    this.getDataByCORS();
  }
  
}
</script>
<style scoped>
.title{
  font-size: 20px;
}
</style>
