<template>
  <div>
    <h1>Репозитории</h1>
    <hr>
    <div v-if="!repositories" class="text-center">
      <p><em>Loading...</em></p>
      <h1><icon icon="spinner" pulse /></h1>
    </div>
    <div v-if="!repositories.length" class="text-center">
      <p><em>Нет данных</em></p>
    </div>
    <template v-if="repositories.length">
      <table class="table">
        <thead class="bg-dark text-white">
          <tr>
            <th></th>
            <th>Логин</th>
            <th>Название</th>
            <th>Описание</th>
            <th>Язык</th>
            <th>Звезды</th>
            <th>Форки</th>
            <th>Обновлен</th>
          </tr>
        </thead>
        <tbody>
          <tr :class="index % 2 == 0 ? 'bg-white' : 'bg-light'" v-for="(repo, index) in repositories" :key="index">
            <td><img :src="repo.authorImg" class="img-responsive rounded-circle" style="max-height: 100px" :alt="repo.authorName" /></td>
            <td>{{ repo.authorName }}</td>
            <td><a :href="repo.url">{{ repo.title }}</a></td>
            <td>{{ repo.description }}</td>
            <td>{{ repo.language }}</td>
            <td>{{ repo.starCount }}</td>
            <td>{{ repo.forkCount }}</td>
            <td>{{ repo.lastUpdate | formatDate }}</td>
          </tr>
        </tbody>
      </table>
    </template>
  </div>
</template>

<script>
export default {
  computed: {

  },

  data () {
    return {
      repositories: null,
    }
  },

  methods: {
    async loadReps() {
      try {

        let response = await this.$http.get(`/api/repository/GetRepositories`)
        console.log(response.data)
        this.repositories = response.data

      } catch (err) {
        window.alert("Серверная ошибка")
        console.log(err)
      }
    }
  },

  async created () {
     this.loadReps()
  },
  async mounted() {
    this.$root.$on('repsLoaded', data => {
       this.loadReps();
    });
    }
}
</script>

<style>
</style>
