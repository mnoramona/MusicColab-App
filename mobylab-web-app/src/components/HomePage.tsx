import React from 'react';

const HomePage: React.FC = () => {
  return (
    <div style={{ textAlign: 'center', marginTop: 40 }}>
      <h1>Welcome to MusicColab!</h1>
      <p style={{ fontSize: 18, margin: '24px 0' }}>
        This is your hub for music collaboration.<br />
        Use the navigation bar to access your Projects, Shared Files, and Collaboration Space.
      </p>
      <p style={{ color: '#888' }}>
        Start by registering or logging in, then create or join a project to begin collaborating with others.
      </p>
    </div>
  );
};

export default HomePage; 