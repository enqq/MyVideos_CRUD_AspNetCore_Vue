import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        video: []
    },
    mutations: {
        createVideo(state, payload){
            state.video.push(payload)
           
        },
        updateVideo(state, payload){
            var index = state.video.findIndex(x=>x.id == payload.id)
            state.video.splice(index, 1, payload)

        }
    },
    actions: {
        createVideo(context, payload){
            
            var { id, title, yearOfProduction } = payload.video
            
                id = 0
             axios.post("api/video", {
                 id,                
                 title,
                 yearOfProduction
             })
             .then(({data}) => {
                    context.commit("createVideo", data)
                })
            .catch(error => {
                console.log(error) 
                 })

        },
        updateVideo(context, payload){
            const {id ,title, yearOfProduction} = payload.video
            const model = {id ,title, yearOfProduction}
           

            axios.put("api/video", {
                id,
                title,
                yearOfProduction
            })
            .then(() => {
                context.commit("updateVideo", model)
            })
            .catch( error => {
                console.log(error)
            })
        }
    }
})