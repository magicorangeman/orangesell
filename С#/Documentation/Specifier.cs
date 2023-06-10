using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Documentation;

public class Specifier<T> : ISpecifier
{
	public string GetApiDescription()
	{
		var attributes = GetType().Name.OfType<ApiDescriptionAttribute>();
		return attributes.First().Description;
	}

	public string[] GetApiMethodNames()
	{
		throw new NotImplementedException();
	}

	public string GetApiMethodDescription(string methodName)
	{
		throw new NotImplementedException();
	}

	public string[] GetApiMethodParamNames(string methodName)
	{
		throw new NotImplementedException();
	}

	public string GetApiMethodParamDescription(string methodName, string paramName)
	{
		throw new NotImplementedException();
	}

	public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
	{
		throw new NotImplementedException();
	}

	public ApiMethodDescription GetApiMethodFullDescription(string methodName)
	{
		throw new NotImplementedException();
	}
}