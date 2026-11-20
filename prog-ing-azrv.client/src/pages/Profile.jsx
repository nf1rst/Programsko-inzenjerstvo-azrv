import { useCallback, useEffect, useState } from 'react';
import './Profile.css';

async function api(path, options = {}) {
    const response = await fetch(path, { credentials: 'include', ...options });
    if (!response.ok) {
        const message = response.status === 401 ? 'Prijava je istekla. Prijavi se ponovno.' :
            response.status === 409 ? 'Ova vještina već je na popisu.' : 'Radnja nije uspjela. Provjeri je li poslužitelj pokrenut.';
        throw new Error(message);
    }
    if (response.status === 204) return null;
    const text = await response.text();
    return text ? JSON.parse(text) : null;
}

function SkillList({ title, skills, kind, onAdd, onRemove }) {
    return <section className="profile-list">
        <div className="profile-section-heading"><h2>{title}</h2><button type="button" className="profile-plus" aria-label={`Dodaj vještinu: ${title}`} onClick={() => onAdd(kind)}>+</button></div>
        {skills.length ? <ul>{skills.map(skill => <li className={kind === 1 ? 'wish' : 'offer'} key={skill.id}>
            <span>{skill.name}</span><button type="button" aria-label={`Ukloni ${skill.name}`} onClick={() => onRemove(skill.id)}>×</button>
        </li>)}</ul> : <p className="profile-empty">Nema dodanih vještina.</p>}
    </section>;
}

export default function Profile({ onSignedOut }) {
    const [profile, setProfile] = useState(null);
    const [catalog, setCatalog] = useState([]);
    const [error, setError] = useState('');
    const [editing, setEditing] = useState(false);
    const [bio, setBio] = useState('');
    const [adding, setAdding] = useState(null);
    const [skillId, setSkillId] = useState('');
    const [level, setLevel] = useState('1');
    const [busy, setBusy] = useState(false);
    const load = useCallback(async () => {
        const data = await api('/api/profile');
        setProfile(data); setBio(data.bio || '');
    }, []);
    useEffect(() => { load().catch(e => setError(e.message)); }, [load]);
    async function perform(operation) {
        setBusy(true); setError('');
        try { await operation(); } catch (e) { setError(e.message); } finally { setBusy(false); }
    }
    async function openAdd(kind) {
        await perform(async () => {
            const data = await api('/api/profile/catalog');
            setCatalog(data); setSkillId(''); setAdding(kind);
        });
    }
    async function addSkill(event) {
        event.preventDefault();
        await perform(async () => {
            await api('/api/profile/skills', { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ skillId: Number(skillId), status: adding, level: Number(level) }) });
            setAdding(null); await load();
        });
    }
    async function removeSkill(id) {
        await perform(async () => { await api(`/api/profile/skills/${id}`, { method: 'DELETE' }); await load(); });
    }
    async function saveBio(event) {
        event.preventDefault();
        await perform(async () => {
            await api('/api/profile/bio', { method: 'PUT', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ bio }) });
            setEditing(false); await load();
        });
    }
    async function logout() {
        await perform(async () => { await api('/api/auth/logout', { method: 'POST' }); onSignedOut(); });
    }
    return <main className="profile-page">
        {error && <div className="profile-error" role="alert">{error} {!profile && <button type="button" onClick={onSignedOut}>Povratak na prijavu</button>}</div>}
        {!profile ? <p>Učitavanje profila…</p> : <>
            <header className="profile-header">
                <div className="profile-avatar" aria-hidden="true">{(profile.userName || '?')[0].toUpperCase()}</div>
                <div className="profile-identity"><h1>{profile.userName || 'Korisnik'}</h1><span className="profile-rating">Bez ocjene</span></div>
                <div className="profile-bio">{editing ? <form onSubmit={saveBio}><label htmlFor="bio">Opis profila</label><textarea id="bio" maxLength="500" value={bio} onChange={e => setBio(e.target.value)} /><button disabled={busy}>Spremi</button><button type="button" onClick={() => { setBio(profile.bio || ''); setEditing(false); }}>Odustani</button></form> : <><p>{profile.bio || 'Nema opisa profila.'}</p><button type="button" onClick={() => setEditing(true)}>Uredi opis</button></>}</div>
            </header>
            <div className="profile-columns">
                <SkillList title="Lista želja" kind={1} skills={profile.skills.filter(s => s.status === 1)} onAdd={openAdd} onRemove={removeSkill} />
                <SkillList title="Lista ponude" kind={2} skills={profile.skills.filter(s => s.status === 2)} onAdd={openAdd} onRemove={removeSkill} />
                <section className="profile-friends"><h2>Prijatelji</h2>
                    {profile.friends.length ? <ul>{profile.friends.map(friend => <li key={friend.id}><span className="friend-avatar" aria-hidden="true">{(friend.name || '?')[0].toUpperCase()}</span><span>{friend.name || 'Korisnik'}</span></li>)}</ul> : <p className="profile-empty">Nema prijatelja za prikaz.</p>}
                    <button className="profile-back" type="button" disabled={busy} onClick={logout}>Odjava</button>
                </section>
            </div>
            {adding !== null && <section className="profile-add" aria-label="Dodavanje vještine"><h2>Dodaj u {adding === 1 ? 'listu želja' : 'listu ponude'}</h2>
                {catalog.length === 0 ? <p>U katalogu još nema vještina.</p> : <form onSubmit={addSkill}>
                    <label>Vještina <select required value={skillId} onChange={e => setSkillId(e.target.value)}><option value="">Odaberi vještinu</option>{catalog.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}</select></label>
                    <label>Razina <select value={level} onChange={e => setLevel(e.target.value)}><option value="1">Početna</option><option value="2">Srednja</option><option value="3">Napredna</option></select></label>
                    <button disabled={busy}>Dodaj</button>
                </form>}
                <button type="button" onClick={() => setAdding(null)}>Zatvori</button>
            </section>}
        </>}
    </main>;
}
