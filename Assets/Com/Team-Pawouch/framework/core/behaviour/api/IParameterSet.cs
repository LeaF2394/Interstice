// (c)2024-2026 Team Pawouch
// IParameterSet.cs - 2026-11-20 00:11:03

#nullable enable
using com.team_pawouch.framework.core.datas.api;

namespace com.team_pawouch.framework.core.behaviour.api;

public interface IParameterSet : ISet<ICastable<string>, ICastable<object>>, IIndexer<ICastable<string>, ICastable<object>>;

// END FILE
