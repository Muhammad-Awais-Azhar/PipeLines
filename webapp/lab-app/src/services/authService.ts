import apiService from './apiservice';

export const login = async (username: string, password: string) => {
    const response = await apiService.post('auth/login', {
        username,
        password
    });
    return response.data.token;
};

export const register = async (userData: FormData) => {
    return apiService.post('auth/register', userData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        }
    });
};
