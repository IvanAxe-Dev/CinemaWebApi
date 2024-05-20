<script setup>
import { defineProps } from 'vue';

const props = defineProps({
  movie: {
    type: Object,
    required: true
  }
});
</script>

<template>
  <div class="movie">
    <h3>This is a movie</h3>
    <div class="poster-wrapper">

    </div>
    <div class="movie-info">
      <span class="info-element">Id: {{ props.movie.id }}</span>
      <span class="info-element">Id: {{ props.movie.title }}</span>
    </div>
  </div>
</template>