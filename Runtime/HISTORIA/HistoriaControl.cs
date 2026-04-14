using UnityEngine;
using Bounds.Infraestructura;
using Ging1991.Dialogos;
using Ging1991.Dialogos.Persistencia;
using Ging1991.Persistencia.Direcciones;
using Bounds.Dialogos;
using Bounds.Persistencia;
using Bounds.Persistencia.Parametros;
using Bounds.Musica;

namespace Bounds.Historia {

	public class HistoriaControl : MonoBehaviour {

		public Dialogo<AccionBounds> dialogo;
		private Configuracion configuracion;
		public ParametrosControl parametrosControl;
		public MusicaDeFondo musicaDeFondo;
		public ControlUIBounds personalizarUI;

		void Start() {
			parametrosControl.Inicializar();
			ParametrosEscena parametros = parametrosControl.parametros;
			personalizarUI.Personalizar(parametros.direcciones["SISTEMA"], parametros.direcciones["COLORES"]);
			configuracion = new(parametros.direcciones["CONFIGURACION"]);
			musicaDeFondo.Inicializar(parametros.direcciones["MUSICA_TIENDA"]);

			DireccionRecursos direccion = new("HISTORIA", $"CAPITULO{configuracion.LeerCapituloHistoria()}");

			LectorLista<AccionBounds> lector1 = new LectorLista<AccionBounds>(
				direccion.Generar(),
				Ging1991.Persistencia.Lectores.TipoLector.RECURSOS
			);

			dialogo.Inicializar(
				lector1.GetLista(),
				new LectorImagenes(new DireccionRecursos("PERSONAJES/MINIATURAS")),
				new LectorImagenes(new DireccionRecursos("PERSONAJES/AVATARES"))
			);

		}


		public void BotonVolver() {
			ControlEscena escena = ControlEscena.GetInstancia();
			escena.CambiarEscena_menu();
		}


	}

}