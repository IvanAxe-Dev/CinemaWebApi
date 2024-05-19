<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import MovieInfo from '../components/MovieInfo.vue';
import { formatPoster } from '@/utils';

const movie = ref(null);
const loading = ref(true);
const error = ref(null);
const router = useRouter();

const fetchMovies = async (movieId) => {
  if (!movieId || 
      router.currentRoute.value.params.id !== movieId) {
    return;
  }

  const url = `api/Movie/${movieId}`;
  loading.value = true;
  error.value = null;

  try {
    const response = await axios.get(url);
    movie.value = response.data;
  } catch (e) {
    error.value = 'Failed to load latest movies';
    console.log(error.value);
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  fetchMovies(router.currentRoute.value.params.id);
});

const posterImage = computed(() => {
  return formatPoster(movie.value.imageUrl, 810, 1200);
});

</script>

<template>
  <div class="movie-details">
    <div class="poster-container" v-if="!loading && movie">
      <img :src="posterImage" alt="Movie Poster">
      <button>Watch trailer</button>
    </div>
    <movie-info :movie="movie" v-if="!loading && movie"></movie-info>
  </div>
</template>

<style scoped lang="scss">
.movie-details {
  display: flex;
  align-items: flex-start;
  max-width: 1600px;
  padding: 20px;
  margin: 0 auto;
}

.poster-container {
  max-width: 400px;
  max-height: 800px;
  border-radius: 15px;

  img {
    width: 100%;
    height: auto;
    border-radius: 15px;
  }

  button {
    padding: 10px 20px;
    border-width: 2px;
    border-radius: 15px;
    color: white;
    background-color: #20232a;
  }
}

.movie-info {
  flex: 1;
  padding: 20px;
}

@media (max-width: 768px) {
  .movie-details {
    flex-direction: column;
    align-items: center;
  }
  .poster-container {
    margin-bottom: 20px;
    width: 100%;

    img {
      width: 100%;
      height: auto;
    }
  }
}
</style>