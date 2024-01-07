namespace Einzel.Services
{
    public class ImagemService
    {
        public string GerarURLDaImagem(int imagemId)
        {
            string baseUrl = "http://localhost:4200";
            string rotaImagens = "api/imagens"; 
            return $"{baseUrl}/{rotaImagens}/{imagemId}";
        }
    }
}
