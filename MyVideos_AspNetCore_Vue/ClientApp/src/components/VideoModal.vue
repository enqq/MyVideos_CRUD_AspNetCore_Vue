<template>

<transition name="modal">



        <div class="modal-mask">
          <div class="modal-wrapper">

              <div class="modal-container" v-if="isLoading">
                  <div class="modal-body">
                    <div class="text-center">
                    <div class="spinner-border" role="status">
                    <span class="sr-only"></span>
                </div>
              </div>
              </div>
            </div>

            <div class="modal-container" v-else>

              <div class="modal-header">
                  <h1 v-if="mode == 'show'">{{ video.title }}</h1>
                  <h1 v-else-if="mode == 'update'">Tryb edycji</h1>
                  <h1 v-else>Dodaj nowy film</h1>
              </div>

              <div class="modal-body">
                        
                        <p v-if="mode == 'show'"> Rok produckji {{ video.yearOfProduction + 'r' }}</p>
                  
                  <form v-else @submit.prevent="update">

                        <div class="mb-3">
                               <label for="name" class="col-form-label" >Tytuł:</label>
                               <input type="text" class="form-control" :class="{ 'is-invalid': $v.video.title.$invalid && $v.video.title.$dirty}" 
                                @input="$v.video.title.$touch()" id="name" v-model.trim="video.title" />
                                <small class="form-text text-muted" v-show="$v.video.title.$dirty && !$v.video.title.required">Pole jest wymagane</small>
                                <small class="form-text text-muted" v-show="$v.video.title.$dirty &&  !$v.video.title.maxLength">
                                    Mayksymalna liczba znaków wynosi {{$v.video.title.$params.maxLength.max}}
                                    </small>
                         </div>
                         <div class="mb-3">
                               <label for="year" class="col-form-label">Rok produkcji:</label>
                               <input type="number" class="form-control" id="year" :class="{ 'is-invalid': $v.video.yearOfProduction.$invalid && $v.video.yearOfProduction.$dirty }"
                                @input="$v.video.yearOfProduction.$touch()" v-model.number.lazy="video.yearOfProduction" />
                                <small class="form-text text-muted" v-show="$v.video.yearOfProduction.$dirty &&  !$v.video.yearOfProduction.between">
                                Akceptowalne ramy czasowe {{$v.video.yearOfProduction.$params.between.min}} - {{$v.video.yearOfProduction.$params.between.max}}
                                </small>
                          </div>
                        
                  </form>
                 
                
                 

              </div>

              <div class="modal-footer">
                  
                  <button class="btn btn-secondary" @click="close()">
                    Zamknij
                  </button>
                  <button  type="submit" v-if="mode == 'update'" class="btn btn-primary" @click="update" :disabled="$v.$invalid">
                  Zatwierdz
                  </button>
                  <button  type="submit" v-if="mode == 'create'" class="btn btn-primary" @click="create" :disabled="$v.$invalid || btnActive">
                  Zatwierdz
                  </button>
              </div>
            </div>
          </div>
        </div>
      </transition>

</template>

<script>

      import { validationMixin } from 'vuelidate'
      import { required, maxLength, between} from 'vuelidate/lib/validators'


export default {
    name: "VideoModal",
    props: ["mode", "videoId"],
    mixins: [validationMixin],
    data(){
          return{
                video: {},
                btnActive: false,
                isLoading: false
          }
    },
    methods: {
          close(){
                this.$emit('close');

          },
          create(){
            this.btnActive = true  

            this.$store.dispatch("createVideo", {
              video: this.video
              })
              .then( () => {
                this.btnActive = false
                this.close()
               
              })
          },
          update(){
            this.btnActive = true

            this.$store.dispatch("updateVideo", {
                video: this.video
            })
            .then(() => {
              this.btnActive = false
              this.close()
            })
          }
          

    },

    validations: {
          video: {
              title:{             
                required,
                maxLength: maxLength(200)
              },
              yearOfProduction: {
                  between: between(1900, 2100)
              }
          }
    },

    mounted() {

      if (this.mode != 'create') {

        this.isLoading = true

        this.axios.get("api/video/" + this.videoId)
        .then(
           ({ data }) => {
            this.video = data
            this.isLoading = false
            })
        .catch(error => {
            console.log(error)
            this.close()
          })
      }
          
    }


}
</script>
<style>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 300px;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
  font-family: Helvetica, Arial, sans-serif;
}

.modal-header h3 {
  margin-top: 0;
  color: #42b983;
}

.modal-body {
  margin: 20px 0;
}

.modal-default-button {
  float: right;
}

/*
 * The following styles are auto-applied to elements with
 * transition="modal" when their visibility is toggled
 * by Vue.js.
 *
 * You can easily play with the modal transition by editing
 * these styles.
 */

.modal-enter {
  opacity: 0;
}

.modal-leave-active {
  opacity: 0;
}

.modal-enter .modal-container,
.modal-leave-active .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}


</style>
