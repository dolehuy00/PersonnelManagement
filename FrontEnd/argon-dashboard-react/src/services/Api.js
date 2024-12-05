import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7297',
});

export default api;
