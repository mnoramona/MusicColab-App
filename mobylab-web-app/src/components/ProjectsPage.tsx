import React, { useState, useEffect } from 'react';

interface Project {
  id: string;
  title: string;
  description?: string;
  createdAt: string;
}

const PAGE_SIZE = 5;

const ProjectsPage: React.FC = () => {
  const [projects, setProjects] = useState<Project[]>([]);
  const [total, setTotal] = useState(0);
  const [search, setSearch] = useState('');
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setLoading(true);
    setError(null);
    const token = localStorage.getItem('token');
    fetch(`/api/Project?Page=${page}&PageSize=${PAGE_SIZE}&Search=${encodeURIComponent(search)}`, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    })
      .then(res => {
        if (!res.ok) throw new Error('Failed to fetch projects');
        return res.json();
      })
      .then(data => {
        setProjects(data.data);
        setTotal(data.totalCount);
        setLoading(false);
      })
      .catch(err => {
        setError(err.message);
        setLoading(false);
      });
  }, [page, search]);

  const pageCount = Math.ceil(total / PAGE_SIZE);

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    setPage(1);
  };

  return (
    <div>
      <h2>🎵 Projects</h2>
      <input
        type="text"
        placeholder="Search by project name..."
        value={search}
        onChange={handleSearch}
        style={{ marginBottom: 16, padding: 6, width: 240 }}
      />
      {loading ? (
        <div>Loading...</div>
      ) : error ? (
        <div style={{ color: 'red' }}>{error}</div>
      ) : (
        <>
          <table style={{ width: '100%', borderCollapse: 'collapse', marginBottom: 16 }}>
            <thead>
              <tr>
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left' }}>Project Name</th>
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left' }}>Description</th>
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left' }}>Created At</th>
                <th style={{ borderBottom: '1px solid #ccc' }}></th>
              </tr>
            </thead>
            <tbody>
              {projects.map(project => (
                <tr key={project.id}>
                  <td>{project.title}</td>
                  <td>{project.description}</td>
                  <td>{new Date(project.createdAt).toLocaleString()}</td>
                  <td>
                    <button style={{ marginRight: 8 }}>Open</button>
                    <button style={{ marginRight: 8 }}>Edit</button>
                    <button>Delete</button>
                  </td>
                </tr>
              ))}
              {projects.length === 0 && (
                <tr><td colSpan={4} style={{ textAlign: 'center' }}>No projects found.</td></tr>
              )}
            </tbody>
          </table>
          <div style={{ display: 'flex', gap: 8, alignItems: 'center' }}>
            <button onClick={() => setPage(p => Math.max(1, p - 1))} disabled={page === 1}>Prev</button>
            <span>Page {page} of {pageCount}</span>
            <button onClick={() => setPage(p => Math.min(pageCount, p + 1))} disabled={page === pageCount}>Next</button>
          </div>
        </>
      )}
    </div>
  );
};

export default ProjectsPage; 