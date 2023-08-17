import React, { useState } from 'react';
import { login } from '../services/authService';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = async () => {
        try {
            const token = await login(username, password);
            console.log("Logged in with token:", token);
            // Store the token in local storage/session or use it as needed
        } catch (error) {
            console.error("Login failed:", error);
        }
    };

    return (
        <div className="container mt-5">
          <div className="row justify-content-center">
            <div className="col-md-6">
              <div className="card">
                <div className="card-body">
                  <h4 className="card-title">Login</h4>
                  <input className="form-control my-2" value={username} onChange={e => setUsername(e.target.value)} placeholder="Username" />
                  <input className="form-control my-2" value={password} onChange={e => setPassword(e.target.value)} placeholder="Password" type="password" />
                  <button className="btn btn-primary btn-block" onClick={handleLogin}>Login</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      );
};

export default Login;
