<template>
<div class="container">
       <div id="app">

            <img src="./assets/logo.png">  
            <VideoList :video="video" :loadingVideo="loadingVideo" />
            
      </div>
</div>
</template>

<script>

import VideoList from "./components/VideoList"

export default {
  name: 'App',
  computed: {
      video(){
        return this.$store.state.video;
      }
  },
  data(){
    return{
      loadingVideo: true
    }
  },
  components: {
    VideoList
  },

  created(){
        this.axios.get("api/video").then(
                  (({data}) => {
                    this.$store.state.video = data
                    this.loadingVideo = false
                  })
        )

  }

}
</script>


