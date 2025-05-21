import React, { useState } from "react";

const LoginForm: React.FC<{ onLogin?: () => void }> = ({ onLogin }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setSuccess(null);
    setLoading(true);
    try {
      const response = await fetch("/api/Authorization/Login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      let data = null;
      const text = await response.text();
      if (text) {
        try {
          data = JSON.parse(text);
        } catch {
          // not JSON
        }
      }

      let token = data?.response?.token;
      if (response.ok && token) {
        localStorage.setItem("token", token);
        setSuccess("Login successful (HTTP 200)");
        if (onLogin) onLogin();
        return;
      }
      setError(
        (data && (data.error || data.message || data.errorMessage)) ||
          "Invalid credentials or unexpected server response." +
            (response.status ? ` (HTTP ${response.status})` : "")
      );
    } catch (err: any) {
      setError(
        err.message
          ? `Unexpected error: ${err.message}`
          : "Login failed due to a network or internal error."
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div
      style={{
        minHeight: "40vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: "#1e1e1e",
        padding: 20,
      }}
    >
      <form
        onSubmit={handleSubmit}
        style={{
          margin: "0 auto",
          width: "100%",
          maxWidth: 340,
          color: "#fff",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <h2 style={{ textAlign: "center", marginBottom: "1.5rem" }}>Login</h2>

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
          style={inputStyle}
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
          style={inputStyle}
        />

        {error && (
          <div
            style={{
              color: "#ff4d4f",
              backgroundColor: "transparent",
              border: "none",
              padding: "0.5rem",
              marginBottom: "1rem",
              borderRadius: 6,
              fontSize: "0.9rem",
              textAlign: "center",
            }}
          >
            {error}
          </div>
        )}
        {success && (
          <div
            style={{
              color: "#4caf50",
              backgroundColor: "transparent",
              border: "none",
              padding: "0.5rem",
              marginBottom: "1rem",
              borderRadius: 6,
              fontSize: "0.9rem",
              textAlign: "center",
            }}
          >
            {success}
          </div>
        )}

        <button
          type="submit"
          disabled={loading}
          style={{
            backgroundColor: "#5c6bc0",
            color: "#fff",
            border: "none",
            borderRadius: 8,
            padding: "0.75rem",
            fontSize: "1rem",
            cursor: "pointer",
            width: "100%",
            transition: "background-color 0.3s",
          }}
        >
          {loading ? "Logging in..." : "Login"}
        </button>
      </form>
    </div>
  );
};

const inputStyle: React.CSSProperties = {
  display: "block",
  width: "100%",
  padding: "0.65rem",
  marginBottom: "1rem",
  borderRadius: 6,
  border: "1px solid #555",
  backgroundColor: "#1e1e1e",
  color: "#fff",
};

export default LoginForm;
