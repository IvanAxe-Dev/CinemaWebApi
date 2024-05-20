<script setup>
import { onMounted, ref } from 'vue';
import axios from 'axios';

const responseData = ref(null); 
const fetchData = async () => {
  const url = `api/Charts/countByYear`;

  try {
    const response = await axios.get(url);
    responseData.value = response.data; 
    console.log(responseData.value)
  } catch (error) {
    console.error(error);
  }
}

onMounted(() => {
  fetchData();
});

</script>

<template>
  <div class="year-chart">
    <h1>Chart</h1>
    <div v-if="responseData" class="chart-container">

    </div>
    <p v-else>Loading...</p>
  </div>
</template>