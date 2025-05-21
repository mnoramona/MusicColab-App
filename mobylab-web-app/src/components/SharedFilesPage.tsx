import React, { useState, useEffect } from 'react';

interface SharedFile {
  id: string;
  name: string;
  description?: string;
  user: { name: string; email: string };
  createdAt: string;
  updatedAt: string;
}

const PAGE_SIZE = 10;

const SharedFilesPage: React.FC = () => {
  const [files, setFiles] = useState<SharedFile[]>([]);
  const [total, setTotal] = useState(0);
  const [search, setSearch] = useState('');
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setLoading(true);
    setError(null);
    const token = localStorage.getItem('token');
    fetch(`/api/UserFile/GetPage?Page=${page}&PageSize=${PAGE_SIZE}&Search=${encodeURIComponent(search)}`, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    })
      .then(res => {
        if (!res.ok) throw new Error('Failed to fetch files');
        return res.json();
      })
      .then(data => {
        setFiles(data.data);
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
      <h2>📁 Shared Files</h2>
      <input
        type="text"
        placeholder="Search by file name or uploader..."
        value={search}
        onChange={handleSearch}
        style={{ marginBottom: 16, padding: 6, width: 260 }}
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
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left' }}>File Name</th>
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left' }}>Uploaded By</th>
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left' }}>Created At</th>
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left' }}>Updated At</th>
                <th style={{ borderBottom: '1px solid #ccc' }}></th>
              </tr>
            </thead>
            <tbody>
              {files.map(file => (
                <tr key={file.id}>
                  <td>{file.name}</td>
                  <td>{file.user.name} ({file.user.email})</td>
                  <td>{new Date(file.createdAt).toLocaleString()}</td>
                  <td>{new Date(file.updatedAt).toLocaleString()}</td>
                  <td>
                    <button style={{ marginRight: 8 }}>Download</button>
                    <button>Preview</button>
                  </td>
                </tr>
              ))}
              {files.length === 0 && (
                <tr><td colSpan={5} style={{ textAlign: 'center' }}>No files found.</td></tr>
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

export default SharedFilesPage; 