import { useState } from 'react';
import './Login.css';

function Login() {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

  
    function handleSubmit(e) {
        e.preventDefault(); //spriječi refresh

        if (!username.trim() || !password) {
            setError('Upiši korisničko ime i lozinku.');
            return;
        }

        setError('');
        console.log('Prijava:', username);
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

                <button className="login-button" type="submit">
                    Ulogiraj se
                </button>

                <p className="login-register">
                    Nemate račun? Registrirajte se ovdje
                </p>
            </form>
        </div>
    );
}

export default Login;