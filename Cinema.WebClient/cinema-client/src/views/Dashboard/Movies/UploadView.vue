<script setup>

</script>

<template>
  <div class="upload">
    <h1>This is upload page</h1>
    <form @submit.prevent="submitMovie" class="movie-form">
      <h2>Search and Categories</h2>
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
      
      <button type="submit" class="btn btn-primary">Get Movie Data</button>
    </form>
  </div>
</template>