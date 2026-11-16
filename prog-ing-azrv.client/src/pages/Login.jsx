import { useState } from 'react';
import './Login.css';

function Login({ onGoToRegister }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    async function handleSubmit(e) {
        e.preventDefault();

        if (!username.trim() || !password) {
            setError('Upiši korisničko ime i lozinku.');
            return;
        }

        setError('');
        setLoading(true);

        try {
            const response = await fetch('/api/auth/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
                body: JSON.stringify({ username: username.trim(), password })
            });

            if (response.ok) {
                console.log('Uspješna prijava:', username);
            } else if (response.status === 400 || response.status === 401) {
                setError('Pogrešno korisničko ime ili lozinka.');
            } else {
                setError('Greška na serveru. Pokušaj ponovno.');
            }
        } catch {
            setError('Server nije dostupan.');
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className="login-page">
            <form className="login-form" onSubmit={handleSubmit}>
                <h1 className="login-title">Ulogiraj se</h1>

                <input
                    className="login-input"
                    type="text"
                    placeholder="Korisničko ime"
                    value={username}
                    onChange={e => setUsername(e.target.value)}
                />

                <input
                    className="login-input"
                    type="password"
                    placeholder="Lozinka"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                />

                {error && <p className="login-error">{error}</p>}

                <button className="login-button" type="submit" disabled={loading}>
                    {loading ? 'Prijava…' : 'Ulogiraj se'}
                </button>

                <p className="login-register">
                    Nemate račun?{' '}
                    <button type="button" className="login-link" onClick={onGoToRegister}>
                        Registrirajte se ovdje
                    </button>
                </p>
            </form>
        </div>
    );
}

export default Login;