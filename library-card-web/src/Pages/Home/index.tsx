import { Link } from "react-router-dom"
import Navbar from "../Navbar"
import Students from "../Students"
import "./style.css"

export default function Home(){
    return(
        <>
        <Navbar/>
        <div className="home-container">
            <div className="home-content">
            <div className="home-descripiton">
                <label>Bem vindo! Allison Sousa Balhs</label>
                <a href="#/">Meu perfil</a>
            </div>
            <div className="home-button">
            <Link to="" className="home-button-new">Nova Carteira</Link>
            <button className="home-button-renew">Renovar Carteira</button>
            </div>
            </div>
        <Students/>
        </div>
        
        </>
        )
}