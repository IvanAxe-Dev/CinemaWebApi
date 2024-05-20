<script setup>
import { reactive } from 'vue';
import axios from 'axios';

const movie = reactive({
  searchTerm: '',
  categories: '',
  dateStartInterval: '',
  dateEndInterval: '',
  timeStartInterval: '',
  timeEndInterval: '',
});

function getMovies() {
  axios({
    method: 'get',
    url: 'api/Movie',
    data: {
      searchTerm: movie.searchTerm,
      categories: movie.categories,
      dateStartInterval: movie.dateStartInterval,
      dateEndInterval: movie.dateEndInterval,
      timeStartInterval: movie.timeStartInterval,
      timeEndInterval: movie.timeEndInterval,
    }
  }).then(response => {
    console.log(response);
  }).catch(error => {
    console.log(error);
  });
}

</script>

<template>
  <div class="get">
    <!--Get-->
    <form class="movie-form" @submit.prevent="getMovies">
      <h2>Get movies with required fields</h2>
      <div class="form-group">
          <label for="searchTerm">Search Term:</label>
          <input v-model="movie.searchTerm" type="text" id="searchTerm" name="searchTerm" class="form-control" />
      </div>
      <div class="form-group">
          <label for="categories">Categories:</label>
          <input v-model="movie.categories" type="text" id="categories" name="categories" class="form-control" />
      </div>
      <div class="form-group">
          <label for="dateStartInterval">Date Start Interval:</label>
          <input v-model="movie.dateStartInterval" type="datetime-local" id="dateStartInterval" name="dateStartInterval" class="form-control" />
      </div>
      <div class="form-group">
          <label for="dateEndInterval">Date End Interval:</label>
          <input v-model="movie.dateEndInterval" type="datetime-local" id="dateEndInterval" name="dateEndInterval" class="form-control" />
      </div>
      <div class="form-group">
          <label for="timeStartInterval">Time Start Interval:</label>
          <input v-model="movie.timeStartInterval" type="time" id="timeStartInterval" name="timeStartInterval" class="form-control" />
      </div>
      <div class="form-group">
          <label for="timeEndInterval">Time End Interval:</label>
          <input v-model="movie.timeEndInterval" type="time" id="timeEndInterval" name="timeEndInterval" class="form-control" />
      </div>
      
      <button type="submit" class="button button-primary">Get Movie Data</button>
    </form>
  </div>
</template>

<style scoped lang="scss">
@import '../styles/forms.scss';
</style>