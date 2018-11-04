<template>
    <div class="main-nav">
      <nav class="navbar navbar-expand-md navbar-dark bg-dark">
        <p class="navbar-brand" to="/"> DAN Test</p>

        <form @submit.prevent="loadReps">
          <div class="form-group">
            <label for="term" class="text-light">Запрос</label>
            <input v-model="request.term" type="text" class="form-control" name="term" value="" pattern=".{3,50}" required/>
          </div>
          <div class="form-group">
            <label for="lang"  class="text-light">Язык программирования</label>
            <select class="form-control" v-model.number="request.language" name="lang">

              <option v-for="(lang, index) in langs" :key="index" :value="lang.id">{{lang.title}}</option>
            </select>
          </div>
          <div class="form-group">
            <label class="text-light">Форки</label>
            <div class="row">
              <label for="forkFrom" class="text-light col-1 col-form-label">От</label>
              <div class="col-5">
                <input type="number" v-model.number="request.forkFrom" class="form-control" :class="!forkValid ? 'border-danger' : ''" name="forkFrom" min="0" max="2147483647" />
              </div>
              <label for="forkTo" class="text-light col-1 col-form-label">До</label>
              <div class="col-5">
                <input type="number" v-model.number="request.forkTo" class="form-control" :class="!forkValid ? 'border-danger' : ''" name="forkTo" min="0" max="2147483647"/>
              </div>
            </div>

          </div>
          <div class="form-group">
            <label class="text-light">Звезды</label>
            <div class="row">
              <label for="forkFrom" class="text-light col-1 col-form-label">От</label>
              <div class="col-5">
                <input type="number" v-model.number="request.starsFrom" class="form-control" :class="!starsValid ? 'border-danger' : ''" name="starsFrom" min="0" max="2147483647" />
              </div>
              <label for="forkFrom" class="text-light col-1 col-form-label">До</label>
              <div class="col-5">
                <input type="number" v-model.number="request.starsTo" class="form-control" :class="!starsValid ? 'border-danger' : ''" name="starsTo" min="0" max="2147483647"/>
              </div>
            </div>

          </div>
          <div class="form-group">
            <label class="text-light">Размер (kb)</label>
            <div class="row">
              <label for="forkFrom" class="text-light col-1 col-form-label">От</label>
              <div class="col-5">
                <input type="number" v-model.number="request.sizeFrom" class="form-control" :class="!sizeValid ? 'border-danger' : ''" name="sizeFrom" min="0" max="2147483647"/>
              </div>
              <label for="forkFrom" class="text-light col-1 col-form-label">До</label>
              <div class="col-5">
                <input type="number" v-model.number="request.sizeTo" class="form-control" :class="!sizeValid ? 'border-danger' : ''" name="sizeTo" min="0" max="2147483647"/>
              </div>
            </div>
          </div>
          <div class="form-check">
            <label class="form-check-label text-light">
              <input type="checkbox" class="form-check-input" v-model="request.isArchived">
              Архивные
            </label>
          </div>
          <button class="btn btn-primary btn-block"  type="submit">Отправить</button>
          <p v-if="request.timeStamp" class="text-light">Последнее обновление: {{request.timeStamp | formatDate}}</p>
        </form>
      </nav>

    </div>
</template>

<script>
    

    export default {
      data () {
        return {
          langs: null,
          request: {}
        }
    },
    computed: {
      forkValid() {
        return !this.request.forkTo || this.request.forkTo >= this.request.forkFrom;
      },
      starsValid() {
        return !this.request.starsTo || this.request.starsTo >= this.request.starsFrom;
      },
      sizeValid() {
        return !this.request.sizeTo || this.request.sizeTo >= this.request.sizeFrom;
      }
    },
    methods: {
      async loadReps(e) {
        e.preventDefault();
        if (!this.forkValid || !this.starsValid || !this.sizeValid) {
          alert("Введите корректные данные");
          return;
        }
        
        try {
          
          let res = await this.$http.post("/api/repository/LoadRepositories", this.request);
          this.request.timeStamp = res.data.timeStamp;
            this.$root.$emit('repsLoaded');
            
          } catch (err) {
          window.alert("Серверная ошибка")
            console.log(err)
          }
        },
        async getLangs() {
          try {

            let response = await this.$http.get(`/api/repository/GetLanguages`)
            console.log(response.data)
            this.langs = response.data;
            this.langs.unshift({ id: -1, title: "Любой"});

          } catch (err) {
            window.alert("Серверная ошибка")
            console.log(err)
          }
        },
        async getLastRequest() {
        try {

          let response = await this.$http.get(`/api/repository/GetLastRequest`)
          console.log(response.data)
          this.request = response.data || {language: -1};

        } catch (err) {
          window.alert(err)
          console.log(err)
        }
      }
      },
    async created() {
      this.getLangs();
      this.getLastRequest();
    }

    }
</script>

<style scoped>
    .slide-enter-active, .slide-leave-active {
    transition: max-height .35s
    }
    .slide-enter, .slide-leave-to {
    max-height: 0px;
    }

    .slide-enter-to, .slide-leave {
    max-height: 20em;
    }
</style>
