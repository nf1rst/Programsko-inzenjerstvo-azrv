import { useState } from 'react';
import './Login.css';
import './Register.css';

// Prijevod kodova grešaka koje vraća ASP.NET Identity (/api/auth/register)
const SERVER_ERRORS = {
    DuplicateUserName: 'Račun s ovim e-mailom već postoji.',
    DuplicateEmail: 'Račun s ovim e-mailom već postoji.',
    InvalidEmail: 'E-mail nije ispravan.',
    InvalidUserName: 'E-mail nije ispravan.',
    PasswordTooShort: 'Lozinka mora imati barem 5 znakova.',
    PasswordRequiresNonAlphanumeric: 'Lozinka mora sadržavati barem jedan poseban znak (npr. !, #, _).',
    PasswordRequiresDigit: 'Lozinka mora sadržavati barem jednu znamenku.',
    PasswordRequiresLower: 'Lozinka mora sadržavati barem jedno malo slovo.',
    PasswordRequiresUpper: 'Lozinka mora sadržavati barem jedno veliko slovo.',
    PasswordRequiresUniqueChars: 'Lozinka mora sadržavati više različitih znakova.'
};

const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

// Ista pravila kao na serveru (Identity default + RequiredLength = 5)
function validatePassword(password) {
    if (password.length < 5) return 'Lozinka mora imati barem 5 znakova.';
    if (!/[a-z]/.test(password)) return SERVER_ERRORS.PasswordRequiresLower;
    if (!/[A-Z]/.test(password)) return SERVER_ERRORS.PasswordRequiresUpper;
    if (!/[0-9]/.test(password)) return SERVER_ERRORS.PasswordRequiresDigit;
    if (!/[^a-zA-Z0-9]/.test(password)) return SERVER_ERRORS.PasswordRequiresNonAlphanumeric;
    return '';
}

function Register({ onGoToLogin, onRegistered }) {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    async function handleSubmit(e) {
        e.preventDefault();

        if (!email.trim() || !password || !confirmPassword) {
            setError('Ispuni sva polja.');
            return;
        }
        if (!EMAIL_REGEX.test(email.trim())) {
            setError('E-mail nije ispravan.');
            return;
        }
        const passwordError = validatePassword(password);
        if (passwordError) {
            setError(passwordError);
            return;
        }
        if (password !== confirmPassword) {
            setError('Lozinke se ne podudaraju.');
            return;
        }

        setError('');
        setLoading(true);

        try {
            const response = await fetch('/api/auth/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email: email.trim(), password })
            });

            if (response.ok) {
                onRegistered?.(email.trim());
            } else if (response.status === 400) {
                const data = await response.json().catch(() => null);
                const codes = data?.errors ? Object.keys(data.errors) : [];
                const messages = codes.map(c => SERVER_ERRORS[c]).filter(Boolean);
                setError(messages.length ? messages.join(' ') : 'Registracija nije uspjela. Provjeri unesene podatke.');
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
        <div className="login-page register-page">
            <form className="login-form" onSubmit={handleSubmit} noValidate>
                <h1 className="login-title">Registracija</h1>

                <input
                    className="login-input"
                    type="email"
                    placeholder="E-mail"
                    autoComplete="email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                />

                <input
                    className="login-input"
                    type="password"
                    placeholder="Lozinka"
                    autoComplete="new-password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                />

                <input
                    className="login-input"
                    type="password"
                    placeholder="Potvrda lozinke"
                    autoComplete="new-password"
                    value={confirmPassword}
                    onChange={e => setConfirmPassword(e.target.value)}
                />

                {error && <p className="login-error">{error}</p>}

                <button className="login-button" type="submit" disabled={loading}>
                    {loading ? 'Registracija…' : 'Registriraj se'}
                </button>

                <p className="login-register">
                    Već imate račun?{' '}
                    <button type="button" className="login-link" onClick={onGoToLogin}>
                        Ulogirajte se ovdje
                    </button>
                </p>
            </form>
        </div>
    );
}

export default Register;
