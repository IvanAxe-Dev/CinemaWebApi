<script setup>
import { defineProps, computed } from 'vue';
import { formatDate, getAverageRating } from '../utils.js';

const props = defineProps({
  movie: {
    type: Object,
    required: true
  }
});

const releaseYear = computed(() => {
  const date = new Date(props.movie.releaseDate);
  return date.getFullYear();
});

const rentalTerm = computed(() => {
  const startDate = formatDate(props.movie.rentalStartDate);
  const endDate = formatDate(props.movie.rentalEndDate);
  return startDate === endDate ? startDate : `${startDate} - ${endDate}`;
});

const rating = computed(() => {
  return getAverageRating(props.movie.ratings);
});

const genres = computed(() => {
  const genresArray = [];
  for (const category of props.movie.categories) {
    genresArray.push(category.name);
  }
  return genresArray.join(', ');
});
</script>

<template>
  <div v-if="movie" class="movie-info">
    <h1>{{ movie.title }}</h1>
    <div class="info-content">
      <div class="labels">
        <div class="label">Age:</div>
        <div class="label">Year:</div>
        <div class="label">Title:</div>
        <div class="label">Director:</div>
        <div class="label">Rental term:</div>
        <div class="label">Ranking:</div>
        <div class="label">Genre:</div>
      </div>
      <div class="values">
        <div class="value">{{ movie.ageRestriction }}</div>
        <div class="value">{{ releaseYear }}</div>
        <div class="value">{{ movie.title }}</div>
        <div class="value">{{ movie.director }}</div>
        <div class="value">{{ rentalTerm }}</div>
        <div class="value">{{ rating }}</div>
        <div class="value">{{ genres }}</div>
      </div>
    </div>
    <div class="movie-description">
      <span class="description">{{ movie.description }}</span>
    </div>
  </div>
  <div v-else-if="loading">Loading...</div>
  <div v-else>Error: {{ error }}</div>
</template>

<style scoped lang="scss">
.movie-info {
  text-align: left;
}

.info-content {
  max-width: 400px;
  display: flex;
  justify-content: space-between;
}

.labels, .values {
  flex: 1;
}

.label, .value {
  margin-bottom: 10px;
}

.label {
  font-weight: bold;
  color: #454545;
}

.value {
  font-weight:400;
}

.movie-description {
  margin-top: 20px;
  white-space: pre;
}
</style>