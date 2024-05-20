<script setup>
import { reactive } from 'vue';

const movie = reactive({
  id: '',
  title: '',
  rentalStartDate: '',
  rentalEndDate: '',
  description: '',
  imageUrl: '',
  releaseDate: '',
  director: '',
  duration: '',
  ageRestriction: '',
  trailerUrl: '',
  actors: ''
});
</script>

<template>
  <div class="update">
    <form class="movie-form">
      <!-- Put -->
      <h2>Upload Movie</h2>
      <div class="form-group">
          <label for="id">ID:</label>
          <input v-model="movie.id" type="text" id="id" name="id" class="form-control" />
      </div>
      <div class="form-group">
          <label for="title">Title:</label>
          <input v-model="movie.title" type="text" id="title" name="title" class="form-control" />
      </div>
      <div class="form-group">
          <label for="rentalStartDate">Rental Start Date:</label>
          <input v-model="movie.rentalStartDate" type="datetime-local" id="rentalStartDate" name="rentalStartDate" class="form-control" />
      </div>
      <div class="form-group">
          <label for="rentalEndDate">Rental End Date:</label>
          <input v-model="movie.rentalEndDate" type="datetime-local" id="rentalEndDate" name="rentalEndDate" class="form-control" />
      </div>
      <div class="form-group">
          <label for="description">Description:</label>
          <input v-model="movie.description" type="text" id="description" name="description" class="form-control" />
      </div>
      <div class="form-group">
          <label for="imageUrl">Image URL:</label>
          <input v-model="movie.imageUrl" type="text" id="imageUrl" name="imageUrl" class="form-control" />
      </div>
      <div class="form-group">
          <label for="releaseDate">Release Date:</label>
          <input v-model="movie.releaseDate" type="datetime-local" id="releaseDate" name="releaseDate" class="form-control" />
      </div>
      <div class="form-group">
          <label for="director">Director:</label>
          <input v-model="movie.director" type="text" id="director" name="director" class="form-control" />
      </div>
      <div class="form-group">
          <label for="duration">Duration:</label>
          <input v-model="movie.duration" type="text" id="duration" name="duration" class="form-control" />
      </div>
      <div class="form-group">
          <label for="ageRestriction">Age Restriction:</label>
          <input v-model="movie.ageRestriction" type="number" id="ageRestriction" name="ageRestriction" class="form-control" />
      </div>
      <div class="form-group">
          <label for="trailerUrl">Trailer URL:</label>
          <input v-model="movie.trailerUrl" type="text" id="trailerUrl" name="trailerUrl" class="form-control" />
      </div>
      <div class="form-group">
          <label for="actors">Actors:</label>
          <input v-model="movie.actors" type="text" id="actors" name="actors" class="form-control" />
      </div>
      <!-- Get -->
      <button type="submit" class="btn btn-primary">{{ movie.id ? 'Update' : 'Add' }} Movie</button>
    </form>
  </div>
</template>