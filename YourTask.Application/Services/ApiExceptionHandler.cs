using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourTask.Application.Services
{
    public class ApiExceptionHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage? response = null;

            try
            {
                // Enviar a requisição e obter a resposta
                response = await base.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    throw new ApiException(response.StatusCode, content);
                }

                return response;
            }
            catch (ApiException ex)
            {
                // Lidar com exceções específicas da API
                HandleApiException(ex);
            }
            catch (HttpRequestException ex)
            {
                // Lidar com erros relacionados à rede
                ShowErrorMessage($"Não foi possível comunicar-se com o servidor. Serviço offline ou servidor inacessível. {ex.StatusCode}");
                response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Serviço indisponível"),
                };
            }
            catch (Exception ex)
            {
                // Lidar com erros inesperados
                ShowErrorMessage($"Erro Inesperado: {ex.Message}");
            }

            return response!;
        }

        private void HandleApiException(ApiException ex)
        {
            switch (ex.StatusCode)
            {
                case HttpStatusCode.BadRequest: // 400
                    ShowErrorMessage("Requisição Inválida (400): O servidor não conseguiu entender a solicitação.");
                    break;

                case HttpStatusCode.Unauthorized: // 401
                    ShowErrorMessage("Não Autorizado (401): Você não tem permissão para realizar esta ação.");
                    break;

                case HttpStatusCode.Forbidden: // 403
                    ShowErrorMessage("Proibido (403): Você não tem acesso a este recurso.");
                    break;

                case HttpStatusCode.NotFound: // 404
                    ShowErrorMessage("Não Encontrado (404): O recurso solicitado não foi encontrado.");
                    break;

                case HttpStatusCode.InternalServerError: // 500
                    ShowErrorMessage("Erro Interno do Servidor (500): Ocorreu um erro inesperado no servidor.");
                    break;

                default:
                    if ((int)ex.StatusCode >= 400 && (int)ex.StatusCode < 500)
                    {
                        ShowErrorMessage($"Erro do Cliente ({ex.StatusCode}): {ex.Message}");
                    }
                    else if ((int)ex.StatusCode >= 500)
                    {
                        ShowErrorMessage($"Erro do Servidor ({ex.StatusCode}): {ex.Message}");
                    }
                    else
                    {
                        ShowErrorMessage($"Erro Inesperado ({ex.StatusCode}): {ex.Message}");
                    }
                    break;
            }
        }

        private void ShowErrorMessage(string message)
        {
            // Usar o MessageBox do HandyControl para exibir mensagens de erro
            MessageBox.Error(message, "Erro");
        }
    }

    // Classe de exceção personalizada para erros de API
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string Content { get; }

        public ApiException(HttpStatusCode statusCode, string content)
            : base($"Erro de API: {statusCode}, Conteúdo: {content}")
        {
            StatusCode = statusCode;
            Content = content;

        }
    }
}
