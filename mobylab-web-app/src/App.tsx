import './App.css'
import NavBar from './components/NavBar'
import ProjectsPage from './components/ProjectsPage'
import SharedFilesPage from './components/SharedFilesPage'
import CollaborationPage from './components/CollaborationPage'
import RegisterForm from './components/RegisterForm'
import LoginPage from './components/LoginPage'
import HomePage from './components/HomePage'
import { Routes, Route } from 'react-router-dom'

function App() {
  return (
    <>
      <NavBar />
      <div className="app-content">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/projects" element={<ProjectsPage />} />
          <Route path="/shared-files" element={<SharedFilesPage />} />
          <Route path="/collaboration" element={<CollaborationPage />} />
          <Route path="/register" element={<RegisterForm />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="*" element={<HomePage />} />
        </Routes>
      </div>
    </>
  )
}

export default App
