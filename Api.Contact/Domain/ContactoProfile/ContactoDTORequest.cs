namespace Api.Contact.Domain.ContactoProfile
{
    public class ContactoDTORequest
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public byte[] Foto { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
