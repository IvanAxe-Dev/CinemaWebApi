<script setup>
import { useRouter } from 'vue-router';
import MovieCard from '../components/MovieCard.vue';

const router = useRouter();

function goToDetails(movieId) {
  router.push({ name: 'movie-details', params: { id: movieId}} )
}

</script>

<template>
  <div class="home">
    <h1>Movies</h1>
    <div class="movie-grid">
      <movie-card v-for="movie in movies" :key="movie.id" :movie="movie" @navigateToDetails="goToDetails" />
    </div>
  </div>
</template>

<style scoped>

.movie-grid {
  padding: 20px;
  padding-top: 30px;
  justify-content: center;
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 300px));
  gap: 20px;
}
</style>
