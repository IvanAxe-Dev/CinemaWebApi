<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import MovieInfo from '../components/MovieInfo.vue';

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
    console.log(response);
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

</script>

<template>
  <div class="movie-details">
    <div class="poster-container" v-if="!loading && movie">
      <img :src="movie.imageUrl">
      <button>Watch trailer</button>
    </div>
    <movie-info :movie="movie" v-if="!loading && movie"></movie-info>
  </div>
</template>

<style scoped>
.movie-details {
  display: flex;
  align-items: flex-start;
  max-width: 1600px;
  padding: 20px;
  margin: 0 auto;
}

.poster-container {
  position: fixed;
  left: 40px;
  width: 300px;
  height: 600px;
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
  margin-left: 360px;
  padding: 20px;
}

@media (max-width: 768px) {
  .movie-details {
    flex-direction: column;
  }
  .poster-container {
    position: static;
    margin-bottom: 20px;
    width: 100%;

    img {
      height: 100%;
      width: auto;
    }
  }
  .movie-info {
    margin-left: 0;
  }
}
</style>