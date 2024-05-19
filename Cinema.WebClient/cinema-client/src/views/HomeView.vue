<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import MovieCard from '../components/MovieCard.vue';

const movies = ref([]);
const loading = ref(true);
const error = ref(null);
const router = useRouter();

const fetchMovies = async (numberOfMovies) => {
  const url = `api/Movie/GetLatestMovies?moviesToTake=${numberOfMovies}`;
  loading.value = true;
  error.value = null;

  try {
    const response = await axios.get(url);
    movies.value = response.data;
  } catch (e) {
    error.value = 'Failed to load latest movies';
  } finally {
    loading.value = false;
  }
}

function goToDetails(movieId) {
  router.push({ name: 'movie-details', params: { id: movieId}} )
}

onMounted(() => {
  fetchMovies(100);
});

</script>

<template>
  <div class="home">
    <h1>Movies</h1>
    <div class="movie-grid">
      <movie-card v-for="movie in movies" :key="movie.id" :movie="movie" @navigateToDetails="goToDetails" />
    </div>
  </div>
</template>

<style scoped lang="scss">

.movie-grid {
  padding: 20px;
  padding-top: 30px;
  justify-content: center;
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 300px));
  gap: 20px;
}
</style>
