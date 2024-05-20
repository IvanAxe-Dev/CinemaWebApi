<script setup>
import { onMounted, computed, ref } from 'vue';
import axios from 'axios';
import { Bar } from 'vue-chartjs';
import { Chart, BarElement, CategoryScale, LinearScale, Tooltip, Legend } from 'chart.js';

Chart.register(BarElement, CategoryScale, LinearScale, Tooltip, Legend);

const responseData = ref(null); 

const fetchData = async () => {
  const url = `api/Charts/countByYear`;

  try {
    const response = await axios.get(url);
    responseData.value = response.data; 
  } catch (error) {
    console.error(error);
  }
}

onMounted(() => {
  fetchData();
});

const chartData = computed(() => {
  if (!responseData.value) return null;
  
  return {
    labels: responseData.value.map(item => item.year),
    datasets: [
      {
        label: "Number of movies",
        backgroundColor: "#42b983",
        data: responseData.value.map(item => item.count)
      }
    ]
  }
});

const chartOptions = {
  responsive: true,
  maintainAspectRation: false
}

</script>

<template>
  <div class="releases-chart">
    <h1>Number of movies released by year</h1>
    <div v-if="responseData" class="chart-container">
      <Bar v-if="chartData" :data="chartData" :options="chartOptions"></Bar>
    </div>
    <p v-else>Loading...</p>
  </div>
</template>