<script setup>
import { onMounted, computed, ref } from 'vue';
import axios from 'axios';
import { Bar } from 'vue-chartjs';
import { Chart, BarElement, CategoryScale, LinearScale, Tooltip, Legend } from 'chart.js';
import { combineArrays } from '../../../utils';

Chart.register(BarElement, CategoryScale, LinearScale, Tooltip, Legend);

const responseData = ref(null); 

const value = (ticketsSold, price) => {
  console.log(ticketsSold)
  return ticketsSold * price;
}

const fetchData = async () => {
  const url = `api/Charts/seatsByYear`;

  try {
    const response = await axios.get(url);
    responseData.value = combineArrays(response.data); 
  } catch (error) {
    console.error(error);
  }
}

onMounted(() => {
  fetchData();
});

const chartData = computed(() => {
  if (!responseData.value) return null;
  
  console.log(responseData.value);

  return {
    labels: responseData.value.map(item => item.ticketsSold),
    datasets: [
      {
        label: "Value",
        backgroundColor: "#42b983",
        data: responseData.value.map(item => value(item.ticketsSold, item.price))
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
  <div class="year-chart">
    <h1>Number of movies released by year</h1>
    <div v-if="responseData" class="chart-container">
      <Bar v-if="chartData" :data="chartData" :options="chartOptions"></Bar>
    </div>
    <p v-else>Loading...</p>
  </div>
</template>