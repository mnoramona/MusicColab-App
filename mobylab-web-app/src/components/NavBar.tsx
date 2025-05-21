import React from 'react';
import { NavLink } from 'react-router-dom';

const navBarHeight = 56;

const NavBar: React.FC = () => {
  return (
    <nav
      style={{
        position: 'fixed',
        top: 0,
        left: 0,
        width: '100%',
        height: navBarHeight,
        display: 'flex',
        alignItems: 'center',
        gap: 32,
        padding: '0 32px',
        background: '#f0f0f0',
        boxShadow: '0 2px 8px rgba(0,0,0,0.07)',
        zIndex: 1000,
      }}
    >
      <NavLink to="/" style={({ isActive }) => ({ fontWeight: isActive ? 'bold' : 'normal', marginRight: 20 })} end>
        Home
      </NavLink>
      <NavLink to="/projects" style={({ isActive }) => ({ fontWeight: isActive ? 'bold' : 'normal', marginRight: 20 })}>
        🎵 Projects
      </NavLink>
      <NavLink to="/shared-files" style={({ isActive }) => ({ fontWeight: isActive ? 'bold' : 'normal', marginRight: 20 })}>
        📁 Shared Files
      </NavLink>
      <NavLink to="/collaboration" style={({ isActive }) => ({ fontWeight: isActive ? 'bold' : 'normal', marginRight: 20 })}>
        💬 Collaboration Space
      </NavLink>
      <NavLink to="/login" style={({ isActive }) => ({ fontWeight: isActive ? 'bold' : 'normal' })}>
        Login
      </NavLink>
    </nav>
  );
};

export default NavBar; 