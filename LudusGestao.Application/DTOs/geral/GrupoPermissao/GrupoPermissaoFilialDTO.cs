using System;
using System.Collections.Generic;

namespace LudusGestao.Application.DTOs.geral.GrupoPermissao
{
    public class GrupoPermissaoFilialDTO
    {
        public Guid Id { get; set; }
        public Guid GrupoPermissaoId { get; set; }
        public Guid FilialId { get; set; }
        public List<Guid> Permissoes { get; set; } = new List<Guid>();
        public int TenantId { get; set; }
    }

    public class CreateGrupoPermissaoFilialDTO
    {
        public Guid GrupoPermissaoId { get; set; }
        public Guid FilialId { get; set; }
        public List<Guid> Permissoes { get; set; } = new List<Guid>();
    }

    public class UpdateGrupoPermissaoFilialDTO
    {
        public List<Guid> Permissoes { get; set; } = new List<Guid>();
    }
}
