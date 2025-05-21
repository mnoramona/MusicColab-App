import React from 'react';
import LoginForm from './LoginForm';

const LoginPage: React.FC = () => {
  return (
    <div style={{ maxWidth: 420, margin: '40px auto', padding: 14, background: 'rgb(50 48 48)', borderRadius: 8, boxShadow: '0 2px 12px #0001' }}>
      <h1 style={{ textAlign: 'center', marginBottom: 24 }}>Sign in to MusicColab</h1>
      <LoginForm onLogin={() => window.location.href = '/'} />
      <div style={{ textAlign: 'center', marginTop: 16 }}>
        <span>Don&apos;t have an account? <a href="/register">Register</a></span>
      </div>
    </div>
  );
};

export default LoginPage; 