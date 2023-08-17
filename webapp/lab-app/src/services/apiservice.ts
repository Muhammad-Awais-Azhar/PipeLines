import axios from 'axios';

const BASE_URL = 'https://localhost:7102/api';

const apiService = axios.create({
    baseURL: BASE_URL,
});

// Global error handling
apiService.interceptors.response.use(
    response => response.data,
    error => {
        // Handle errors globally
        console.error("Global response error", error);
        return Promise.reject(error);
    }
);

export default apiService;