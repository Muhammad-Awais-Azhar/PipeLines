import React, { useCallback, useState } from 'react';
import { useDropzone } from 'react-dropzone';
import { register } from '../services/authService';

const Register = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [profileImage, setProfileImage] = useState<File | null>(null);

    const onDrop = useCallback((acceptedFiles: File[]) => {
        setProfileImage(acceptedFiles[0]);
    }, []);
    const { getRootProps, getInputProps } = useDropzone({ onDrop, maxFiles: 1 });

    const handleRegister = async () => {
        const formData = new FormData();
        formData.append('username', username);
        formData.append('password', password);
        if (profileImage) {
            formData.append('profileImage', profileImage);
        }
        
        try {
            await register(formData);
            console.log("Registered successfully");
        } catch (error) {
            console.error("Registration failed:", error);
        }
    };

    return (
        <div className="container mt-5">
          <div className="row justify-content-center">
            <div className="col-md-6">
              <div className="card">
                <div className="card-body">
                  <h4 className="card-title">Register</h4>
                  <input className="form-control my-2" value={username} onChange={e => setUsername(e.target.value)} placeholder="Username" />
                  <input className="form-control my-2" value={password} onChange={e => setPassword(e.target.value)} placeholder="Password" type="password" />
                  <div {...getRootProps()} className="dropzone my-2">
                    <input {...getInputProps()} />
                    {profileImage ? <img className="img-fluid" src={URL.createObjectURL(profileImage)} alt="Profile Preview" /> : <p>Drag 'n' drop profile image here, or click to select one</p>}
                  </div>
                  <button className="btn btn-primary btn-block" onClick={handleRegister}>Register</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      );
};

export default Register;
