export const authAPI = {
  register: async (name: string, email: string, password: string) => {
    const response = await fetch('/api/User/Add', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name, email, password, role: 0 }),
    });

    if (!response.ok) {
      let errorMessage = 'Registration failed';
      try {
        const data = await response.json();
        errorMessage = data?.message || errorMessage;
      } catch {
        // Response not in JSON format
      }
      throw new Error(errorMessage);
    }

    return response.json(); // optional, if backend returns something
  },

}; 