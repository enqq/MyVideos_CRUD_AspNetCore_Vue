<template>
<div>
  <button type="button" class="btn btn-success" @click="modalAction(null,'create')">Dodaj</button>
    <table class="table table-striped">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">Tytuł</th>
      <th scope="col">Rok produkcji</th>
      <th scope="col"></th>
    </tr>
  </thead>
  <tbody>

    <tr v-if="loadingVideo" >
      <td colspan="100%">
    <div class="text-center">
      <div class="spinner-border" role="status">
    <span class="sr-only">.</span>
      </div>
    </div>
    </td>
  </tr>

    <tr v-for="(item, index) in  video" :key="item.id" v-else>
      <th scope="row">{{ index + 1 }}</th>
      <td>{{ item.title}}</td>
      <td>{{ item.yearOfProduction }}</td>
      <td><button type="button" class="btn btn-sm btn-info text-white" @click="modalAction(item.id, 'show')">Podgląd</button>
          <button type="button" class="btn btn-sm btn-warning" @click="modalAction(item.id, 'update')">Edytuj</button>
      </td>
    </tr>

  </tbody>
</table>
      <VideoModal v-if="isActive" @close="closeModal" :mode="mode" :videoId="videoId" />
</div>
</template>

<script>
import VideoModal from './VideoModal.vue'

export default{
    name: "VideoList",
    props: ["video", "loadingVideo"],
    data(){
         return{
                isActive: false,
                mode: null,
                videoId: Number,
                videoItem: {
                   id: Number,
                    title: String,
                    yearOfProduction: Number
                    }
                
         }
    },
    methods: {
      closeModal(){
        this.isActive = false;
      },
      modalAction(item, mode) {
        
          this.videoId = mode == "show" || mode == "update" ? item : null;
          this.mode = mode;
          this.isActive = true;
          
      }

    },
    components: {
          VideoModal
    },


}

</script>
