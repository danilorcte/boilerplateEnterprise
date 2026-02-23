import axios from 'axios';

export const http = axios.create({
  baseURL: '/api',
  withCredentials: true
});

http.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const refreshResponse = await axios.post('/api/auth/refresh', {}, { withCredentials: true });
      originalRequest.headers.Authorization = `Bearer ${refreshResponse.data.accessToken}`;
      return http(originalRequest);
    }

    return Promise.reject(error);
  }
);
