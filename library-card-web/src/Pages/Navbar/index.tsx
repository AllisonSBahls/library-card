import "./style.css"

export default function Navbar(){
    return(
      <nav className="navbar">
        <label className="navbar-title">Mark Equip</label>
          <ul className="navbar-menu">
                {/* <li><a href="/">Inicio</a></li>
                <li><a href="/">Carteirinhas</a></li>
                <li><a href="/">Vencidas</a></li> */}
                <li><a href="/Sair">Logout</a></li>
        </ul>
        </nav>    
        )
}